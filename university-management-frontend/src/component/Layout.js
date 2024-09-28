import React from "react";
import { Box, Container, Heading } from "@chakra-ui/react";
import Navbar from "./Navbar";

const Layout = ({ children }) => {
  return (
    <Box>
      {/* Header */}
      <Navbar />

      {/* Main Content */}
      <Box p={6}>{children}</Box>
    </Box>
  );
};

export default Layout;
