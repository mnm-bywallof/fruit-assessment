import Form from "react-bootstrap/Form";
import { observer } from "mobx-react";
import { store } from "../../Store/mainStore";

const _CategoryOptions: React.FC<{ currentOption?: string }> = ({
  currentOption,
}) => {
  return (
    <Form.Select aria-label="Default select example" value={currentOption}>
      {store.categories.map((cat) => (
        <option value={cat.categoryId} key={cat.categoryId}>
          {cat.categoryCode}
        </option>
      ))}
    </Form.Select>
  );
};

export const CategoryOptions = observer(_CategoryOptions);
