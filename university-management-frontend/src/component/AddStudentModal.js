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

const AddStudentModal = ({ onAdd }) => {
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
      // Set a default password for now
      const defaultPassword = "DefaultPassword123";

      await onAdd({
        ...data,
        gender: parseInt(data.gender, 10), // Convert gender to integer
        departmentId: parseInt(data.departmentId, 10), // Convert departmentId to integer
        passwordHash: defaultPassword, // Add default password
      });
      reset();
      setSubmitError(null);
      onClose();
    } catch (error) {
      if (error.response && error.response.data) {
        setSubmitError(`Error: ${error.response.data}`);
      } else {
        setSubmitError("Failed to add student. Please try again.");
      }
    }
  };

  return (
    <>
      <Button onClick={onOpen} colorScheme="teal">
        Add Student
      </Button>

      <Modal isOpen={isOpen} onClose={onClose}>
        <ModalOverlay />
        <ModalContent>
          <ModalHeader>Add New Student</ModalHeader>
          <ModalCloseButton />
          <ModalBody>
            {submitError && <p style={{ color: "red" }}>{submitError}</p>}
            <form id="add-student-form" onSubmit={handleSubmit(onSubmit)}>
              <Stack spacing={4}>
                <FormControl isInvalid={errors.firstName}>
                  <FormLabel>First Name</FormLabel>
                  <Input
                    {...register("firstName")}
                    placeholder="Enter first name"
                  />
                  <p>{errors.firstName?.message}</p>
                </FormControl>

                <FormControl isInvalid={errors.lastName}>
                  <FormLabel>Last Name</FormLabel>
                  <Input
                    {...register("lastName")}
                    placeholder="Enter last name"
                  />
                  <p>{errors.lastName?.message}</p>
                </FormControl>

                <FormControl isInvalid={errors.email}>
                  <FormLabel>Email</FormLabel>
                  <Input {...register("email")} placeholder="Enter email" />
                  <p>{errors.email?.message}</p>
                </FormControl>

                <FormControl isInvalid={errors.doB}>
                  <FormLabel>Date of Birth</FormLabel>
                  <Input type="date" {...register("doB")} />
                  <p>{errors.doB?.message}</p>
                </FormControl>

                <FormControl isInvalid={errors.gender}>
                  <FormLabel>Gender</FormLabel>
                  <Select {...register("gender")}>
                    <option value="">Select Gender</option>
                    <option value="0">Male</option>
                    <option value="1">Female</option>
                    <option value="2">Other</option>
                  </Select>
                  <p>{errors.gender?.message}</p>
                </FormControl>

                <FormControl isInvalid={errors.address}>
                  <FormLabel>Address</FormLabel>
                  <Input {...register("address")} placeholder="Enter address" />
                  <p>{errors.address?.message}</p>
                </FormControl>

                <FormControl isInvalid={errors.phoneNumber}>
                  <FormLabel>Phone Number</FormLabel>
                  <Input
                    {...register("phoneNumber")}
                    placeholder="Enter phone number"
                  />
                  <p>{errors.phoneNumber?.message}</p>
                </FormControl>

                <FormControl isInvalid={errors.enrollmentDate}>
                  <FormLabel>Enrollment Date</FormLabel>
                  <Input type="date" {...register("enrollmentDate")} />
                  <p>{errors.enrollmentDate?.message}</p>
                </FormControl>

                <FormControl isInvalid={errors.departmentId}>
                  <FormLabel>Department</FormLabel>
                  <Select {...register("departmentId")}>
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
              form="add-student-form"
              mr={3}
            >
              Add
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

export default AddStudentModal;
