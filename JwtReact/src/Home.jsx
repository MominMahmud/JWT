// Home.js
import React, { useState } from "react";
import Button from "react-bootstrap/Button";
import ButtonGroup from "react-bootstrap/ButtonGroup";
import Modal from "react-bootstrap/Modal";
import Login from "./Login";
import Register from "./Register";

function Home() {
  const [selectedAction, setSelectedAction] = useState(null);

  const handleLogin = () => {
    setSelectedAction("login");
  };

  const handleRegister = () => {
    setSelectedAction("register");
  };

  const handleCloseModal = () => {
    setSelectedAction(null);
  };

  const renderModalContent = () => {
    if (selectedAction === "login") {
      return (
        <Modal show={selectedAction === "login"} onHide={handleCloseModal}>
          <Modal.Header closeButton>
            <Modal.Title>Login</Modal.Title>
            Close
          </Modal.Header>
          <Modal.Body>
            <Login />
          </Modal.Body>
        </Modal>
      );
    } else if (selectedAction === "register") {
      return (
        <Modal show={selectedAction === "register"} onHide={handleCloseModal}>
          <Modal.Header closeButton>
            <Modal.Title>Register</Modal.Title>
            Close
          </Modal.Header>
          <Modal.Body>
            <Register />
          </Modal.Body>
        </Modal>
      );
    }
  };

  return (
    <>
      <h1>JWT</h1>
      <ButtonGroup aria-label="Basic example">
        <Button onClick={handleLogin} variant="secondary">
          Login
        </Button>
        <Button onClick={handleRegister} variant="secondary">
          Register
        </Button>
      </ButtonGroup>

      {renderModalContent()}
    </>
  );
}

export default Home;
