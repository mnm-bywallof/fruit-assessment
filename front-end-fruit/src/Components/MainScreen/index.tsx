import React from "react";
import CollapsibleNavbar from "./navbar";
import Table from "react-bootstrap/Table";
import { Container } from "react-bootstrap";
import { observer } from "mobx-react";
import { store } from "../../Store/mainStore";
import { CategoryOptions } from "../Controls/dropdown";
import { ImageHolder } from "../Controls/image";

const _MainScreen: React.FC = () => {
  return (
    <>
      <CollapsibleNavbar />
      <Container>
        <Table striped>
          <thead>
            <tr>
              <th>#</th>
              <th>Code</th>
              <th>Product Name</th>
              <th>Description</th>
              <th>Category Code</th>
              <th>Price</th>
              <th>Image</th>
            </tr>
          </thead>
          <tbody>
            {store.products.map((p, i) => (
              <tr key={p.productId}>
                <td>{i + 1}</td>
                <td>{p.productCode}</td>
                <td>{p.name}</td>
                <td>{p.description}</td>
                <td>
                  <CategoryOptions currentOption={p.categoryID} />
                </td>
                <td>{p.price.toString()}</td>
                <td>
                  <ImageHolder product={p} />
                </td>
              </tr>
            ))}
          </tbody>
        </Table>
      </Container>
    </>
  );
};

const MainScreen = observer(_MainScreen);
export default MainScreen;
