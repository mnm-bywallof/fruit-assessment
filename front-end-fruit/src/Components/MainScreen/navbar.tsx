import Container from "react-bootstrap/Container";
import Nav from "react-bootstrap/Nav";
import Navbar from "react-bootstrap/Navbar";
import NavDropdown from "react-bootstrap/NavDropdown";
import { observer } from "mobx-react";
import { store } from "../../Store/mainStore";
import { Login } from "./login";

function _CollapsibleNavbar() {
  return (
    <Navbar collapseOnSelect expand="lg" className="bg-body-tertiary">
      <Container>
        <Navbar.Brand href="#home">Fruit Assessment</Navbar.Brand>
        <Navbar.Toggle aria-controls="responsive-navbar-nav" />
        <Navbar.Collapse id="responsive-navbar-nav">
          <Nav>
            <Nav.Link href="#deets">Email:</Nav.Link>
            <Login />
            <Nav.Link
              href="#logout"
              onClick={() => {
                if (store.user === null) {
                  store.login("nameisriaz@gmail.com", "huambo#1995");
                } else {
                  store.logout();
                }
              }}
            >
              {store.user === null ? "Login" : "Logout"}
            </Nav.Link>
          </Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
  );
}

const CollapsibleNavbar = observer(_CollapsibleNavbar);

export default CollapsibleNavbar;
