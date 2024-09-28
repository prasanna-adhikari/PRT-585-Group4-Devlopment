import React, { useState, useEffect } from "react";
import {
  Box,
  Heading,
  Table,
  Thead,
  Tbody,
  Tr,
  Th,
  Td,
  Button,
  Stack,
  Tooltip,
  useToast,
} from "@chakra-ui/react";
import AddStudentModal from "../component/AddStudentModal";
import EditStudentModal from "../component/EditStudentModal";
import axios from "axios";
import { format } from "date-fns";
const StudentsPage = () => {
  const [students, setStudents] = useState([]);
  const [departments, setDepartments] = useState({});
  const toast = useToast();
  // Fetch students from backend
  const fetchStudents = async () => {
    try {
      const response = await axios.get("https://localhost:7206/api/students");
      setStudents(response.data);
    } catch (error) {
      console.error("Error fetching students:", error);
    }
  };
  const fetchDepartments = async () => {
    try {
      const response = await axios.get(
        "https://localhost:7206/api/departments"
      );
      const departmentsData = response.data.reduce((acc, department) => {
        acc[department.DepartmentId] = department.DepartmentName;
        return acc;
      }, {});
      setDepartments(departmentsData);
    } catch (error) {
      console.error("Error fetching departments:", error);
    }
  };
  useEffect(() => {
    fetchStudents();
    fetchDepartments();
  }, []);

  // Handle adding a new student
  const handleAddStudent = async (student) => {
    try {
      const response = await axios.post(
        "https://localhost:7206/api/students",
        student
      );
      setStudents([...students, response.data]);
    } catch (error) {
      console.error("Error adding student:", error);
    }
  };

  // Handle deleting a student
  const handleDeleteStudent = async (studentId) => {
    try {
      await axios.delete(`https://localhost:7206/api/students/${studentId}`);
      setStudents(
        students.filter((student) => student.StudentId !== studentId)
      );
      toast({
        title: "Student deleted.",
        description: "Student has been successfully deleted.",
        status: "success",
        duration: 5000,
        isClosable: true,
      });
    } catch (error) {
      console.error("Error deleting student:", error);
      toast({
        title: "Error deleting student.",
        description:
          "An error occurred while deleting the student. Please try again.",
        status: "error",
        duration: 5000,
        isClosable: true,
      });
    }
  };

  return (
    <Box>
      {/* Header */}
      <Stack direction="row" justifyContent="space-between" mb={6}>
        <Heading size="lg">Students</Heading>
        <AddStudentModal onAdd={handleAddStudent} />
      </Stack>

      {/* Students Table */}
      <Table variant="striped" colorScheme="teal">
        <Thead>
          <Tr>
            <Th>Student ID</Th>
            <Th>First Name</Th>
            <Th>Last Name</Th>
            <Th>Email</Th>
            <Th>Date of Birth</Th>
            <Th>Gender</Th>
            <Th>Address</Th>
            <Th>Phone Number</Th>
            <Th>Enrollment Date</Th>
            <Th>Department</Th>
            <Th>Actions</Th>
          </Tr>
        </Thead>
        <Tbody>
          {students.map((student) => (
            <Tr key={student.StudentId}>
              <Td>{student.StudentId}</Td>
              <Td>{student.FirstName}</Td>
              <Td>{student.LastName}</Td>
              <Td>{student.Email}</Td>
              <Td>{format(new Date(student.DoB), "dd/MM/yyyy")}</Td>
              <Td>
                {student.Gender === 0
                  ? "Male"
                  : student.Gender === 1
                  ? "Female"
                  : "Other"}
              </Td>
              <Td>{student.Address}</Td>
              <Td>{student.PhoneNumber}</Td>
              <Td>{format(new Date(student.EnrollmentDate), "dd/MM/yyyy")}</Td>
              <Td>{departments[student.DepartmentId] || "Not Assigned"}</Td>
              <Td>
                <EditStudentModal
                  student={student}
                  onUpdate={async (id, patchData) => {
                    try {
                      await axios.patch(
                        `https://localhost:7206/api/students/${id}`,
                        patchData,
                        {
                          headers: {
                            "Content-Type": "application/json-patch+json",
                          },
                        }
                      );
                      // Optionally, refresh the student list
                      fetchStudents();
                    } catch (error) {
                      console.error("Failed to update student:", error);
                    }
                  }}
                />
                <Button
                  colorScheme="red"
                  size="sm"
                  onClick={() => handleDeleteStudent(student.StudentId)}
                  ml={2}
                >
                  Delete
                </Button>
              </Td>
            </Tr>
          ))}
        </Tbody>
      </Table>
    </Box>
  );
};

export default StudentsPage;
