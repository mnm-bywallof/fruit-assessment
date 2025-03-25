import { observer } from "mobx-react";
import { Product, store } from "../../Store/mainStore";
import { useRef } from "react";
import * as fs from "fs";
import * as path from "path";

const _ImageHolder: React.FC<{ product: Product; url?: string }> = ({
  product,
  url,
}) => {
  const imgInputRef = useRef(null);
  const handleFileChange = async (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    if (event.target.files?.item(0)) {
      const file = event.target.files[0];
      store.uploadImage(product, file, () => {});
    }
  };
  return (
    <div
      onClick={() => {
        if (imgInputRef.current) {
          (imgInputRef.current as any).click();
        }
      }}
    >
      <input type="file" onChange={handleFileChange} ref={imgInputRef} />
      {url && <img src={url} height={25} />}
    </div>
  );
};

export const ImageHolder = observer(_ImageHolder);
