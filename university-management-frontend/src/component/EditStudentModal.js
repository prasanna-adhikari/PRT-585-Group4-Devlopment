import React, { useState, useEffect } from "react";
import {
  Button,
  Modal,
  ModalOverlay,
  ModalContent,
  ModalHeader,
  ModalFooter,
  ModalBody,
  ModalCloseButton,
  FormControl,
  FormLabel,
  Input,
  Select,
  useDisclosure,
  Stack,
} from "@chakra-ui/react";
import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
import axios from "axios";

// Validation Schema
const schema = yup.object().shape({
  firstName: yup.string().required("First name is required"),
  lastName: yup.string().required("Last name is required"),
  email: yup.string().email("Invalid email").required("Email is required"),
  doB: yup.date().required("Date of birth is required"),
  gender: yup
    .number()
    .oneOf([0, 1, 2], "Select a valid gender")
    .required("Gender is required"),
  address: yup.string().required("Address is required"),
  phoneNumber: yup
    .string()
    .matches(/^[0-9]+$/, "Phone number must be only digits")
    .min(10, "Phone number must be at least 10 digits")
    .required("Phone number is required"),
  enrollmentDate: yup.date().required("Enrollment date is required"),
  departmentId: yup.number().required("Department is required"),
});

const EditStudentModal = ({ student, onUpdate }) => {
  const { isOpen, onOpen, onClose } = useDisclosure();
  const [departments, setDepartments] = useState([]);
  const [submitError, setSubmitError] = useState(null);

  const {
    register,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm({
    resolver: yupResolver(schema),
  });

  // Fetch departments when the modal is opened
  useEffect(() => {
    if (isOpen) {
      const fetchDepartments = async () => {
        try {
          const response = await axios.get(
            "https://localhost:7206/api/departments"
          );
          setDepartments(response.data);
        } catch (error) {
          console.error("Error fetching departments:", error);
          setSubmitError("Failed to load departments. Please try again.");
        }
      };

      fetchDepartments();
    }
  }, [isOpen]);

  const onSubmit = async (data) => {
    try {
      const patchData = [];

      // Create a patch array for changed fields
      Object.keys(data).forEach((key) => {
        if (data[key] !== student[key]) {
          patchData.push({
            op: "replace",
            path: `/${key}`,
            value: data[key],
          });
        }
      });

      await onUpdate(student.StudentId, patchData);
      reset();
      setSubmitError(null);
      onClose();
    } catch (error) {
      if (error.response && error.response.data) {
        setSubmitError(`Error: ${error.response.data}`);
      } else {
        setSubmitError("Failed to update student. Please try again.");
      }
    }
  };

  return (
    <>
      <Button onClick={onOpen} colorScheme="teal">
        Edit
      </Button>

      <Modal isOpen={isOpen} onClose={onClose}>
        <ModalOverlay />
        <ModalContent>
          <ModalHeader>Edit Student</ModalHeader>
          <ModalCloseButton />
          <ModalBody>
            {submitError && <p style={{ color: "red" }}>{submitError}</p>}
            <form id="edit-student-form" onSubmit={handleSubmit(onSubmit)}>
              <Stack spacing={4}>
                <FormControl isInvalid={errors.firstName}>
                  <FormLabel>First Name</FormLabel>
                  <Input
                    {...register("firstName")}
                    placeholder="Enter first name"
                    defaultValue={student?.FirstName || ""}
                  />
                  <p>{errors.firstName?.message}</p>
                </FormControl>

                <FormControl isInvalid={errors.lastName}>
                  <FormLabel>Last Name</FormLabel>
                  <Input
                    {...register("lastName")}
                    placeholder="Enter last name"
                    defaultValue={student?.LastName || ""}
                  />
                  <p>{errors.lastName?.message}</p>
                </FormControl>

                <FormControl isInvalid={errors.email}>
                  <FormLabel>Email</FormLabel>
                  <Input
                    {...register("email")}
                    placeholder="Enter email"
                    defaultValue={student?.Email || ""}
                  />
                  <p>{errors.email?.message}</p>
                </FormControl>

                <FormControl isInvalid={errors.doB}>
                  <FormLabel>Date of Birth</FormLabel>
                  <Input
                    type="date"
                    {...register("doB")}
                    defaultValue={
                      student?.DoB
                        ? new Date(student.DoB).toISOString().split("T")[0]
                        : ""
                    }
                  />
                  <p>{errors.doB?.message}</p>
                </FormControl>

                <FormControl isInvalid={errors.gender}>
                  <FormLabel>Gender</FormLabel>
                  <Select
                    {...register("gender")}
                    defaultValue={student?.Gender ?? ""}
                  >
                    <option value="">Select Gender</option>
                    <option value="0">Male</option>
                    <option value="1">Female</option>
                    <option value="2">Other</option>
                  </Select>
                  <p>{errors.gender?.message}</p>
                </FormControl>

                <FormControl isInvalid={errors.address}>
                  <FormLabel>Address</FormLabel>
                  <Input
                    {...register("address")}
                    placeholder="Enter address"
                    defaultValue={student?.Address || ""}
                  />
                  <p>{errors.address?.message}</p>
                </FormControl>

                <FormControl isInvalid={errors.phoneNumber}>
                  <FormLabel>Phone Number</FormLabel>
                  <Input
                    {...register("phoneNumber")}
                    placeholder="Enter phone number"
                    defaultValue={student?.PhoneNumber || ""}
                  />
                  <p>{errors.phoneNumber?.message}</p>
                </FormControl>

                <FormControl isInvalid={errors.enrollmentDate}>
                  <FormLabel>Enrollment Date</FormLabel>
                  <Input
                    type="date"
                    {...register("enrollmentDate")}
                    defaultValue={
                      student?.EnrollmentDate
                        ? new Date(student.EnrollmentDate)
                            .toISOString()
                            .split("T")[0]
                        : ""
                    }
                  />
                  <p>{errors.enrollmentDate?.message}</p>
                </FormControl>

                <FormControl isInvalid={errors.departmentId}>
                  <FormLabel>Department</FormLabel>
                  <Select
                    {...register("departmentId")}
                    defaultValue={student?.DepartmentId ?? ""}
                  >
                    <option value="">Select Department</option>
                    {departments.map((department) => (
                      <option
                        key={department.DepartmentId}
                        value={department.DepartmentId}
                      >
                        {department.DepartmentName}
                      </option>
                    ))}
                  </Select>
                  <p>{errors.departmentId?.message}</p>
                </FormControl>
              </Stack>
            </form>
          </ModalBody>

          <ModalFooter>
            <Button
              colorScheme="teal"
              type="submit"
              form="edit-student-form"
              mr={3}
            >
              Update
            </Button>
            <Button variant="ghost" onClick={onClose}>
              Cancel
            </Button>
          </ModalFooter>
        </ModalContent>
      </Modal>
    </>
  );
};

export default EditStudentModal;
