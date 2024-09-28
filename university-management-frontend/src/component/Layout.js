import React from "react";
import { Box, Container, Heading } from "@chakra-ui/react";
import Navbar from "./Navbar";

const Layout = ({ children }) => {
  return (
    <Box>
      {/* Header */}
      <Navbar />

      {/* Main Content */}
      <Container maxW="container.lg" py={6}>
        {children}
      </Container>
    </Box>
  );
};

export default Layout;
