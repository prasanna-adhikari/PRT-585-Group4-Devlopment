import React from "react";
import { Flex, Box, Link, Button } from "@chakra-ui/react";
import { Link as RouterLink } from "react-router-dom";

const Navbar = () => {
  return (
    <Flex as="nav" bg="teal.600" p={4} justify="space-between" align="center">
      <Box>
        <Link as={RouterLink} to="/" color="white" fontWeight="bold" mr={4}>
          Home
        </Link>
        <Link as={RouterLink} to="/students" color="white" mr={4}>
          Students
        </Link>
        <Link as={RouterLink} to="/courses" color="white" mr={4}>
          Courses
        </Link>
        <Link as={RouterLink} to="/departments" color="white">
          Departments
        </Link>
      </Box>
      <Button colorScheme="teal" variant="outline" size="sm">
        Log In
      </Button>
    </Flex>
  );
};

export default Navbar;
