import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Layout from "./component/Layout";
import StudentsPage from "./pages/StudentsPage";

function App() {
  return (
    <Router>
      <Layout>
        <Routes>
          <Route
            path="/"
            element={<h1>Welcome to the University Management System</h1>}
          />
          <Route path="/students" element={<StudentsPage />} />
          <Route path="/courses" element={<h1>Courses Page</h1>} />
          <Route path="/departments" element={<h1>Departments Page</h1>} />
        </Routes>
      </Layout>
    </Router>
  );
}

export default App;
