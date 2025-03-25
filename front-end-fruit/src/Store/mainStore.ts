import { makeAutoObservable } from "mobx";
import axios from "axios";
import { BlobServiceClient } from "@azure/storage-blob";

const connectionString =
  "BlobEndpoint=https://technicalassessmentfruit.blob.core.windows.net/;QueueEndpoint=https://technicalassessmentfruit.queue.core.windows.net/;FileEndpoint=https://technicalassessmentfruit.file.core.windows.net/;TableEndpoint=https://technicalassessmentfruit.table.core.windows.net/;SharedAccessSignature=sv=2024-11-04&ss=bfqt&srt=sco&sp=rwdlacupiytfx&se=2025-03-25T01:21:34Z&st=2025-03-24T17:21:34Z&spr=https,http&sig=2%2Bq%2FoWHQhe52zUgfXlhRb9gxKashVd060tXXKyAkupI%3D";
const containerName = "images";

export interface Product {
  productId: string; // Guid in C# translates to string in TypeScript for simplicity
  productCode: string;
  name: string;
  description: string;
  categoryID: string;
  price: number;
  image: string;
  categoryCode: string;
}

interface Category {
  categoryId: string; // Guid in C# translates to string in TypeScript for simplicity
  name: string;
  categoryCode: string;
  isActive: number; // int in C# translates to number in TypeScript
}

interface User {
  Uid: string; // Guid in C# translates to string in TypeScript for simplicity
  Email: string;
  Password: string;
}

class StateManager {
  categories: Category[] = [];
  products: Product[] = [];
  user: User | null = null;
  constructor() {
    makeAutoObservable(this);
    this.fetchProducts();
    this.fetchCategories();
  }

  private fetchCategories() {
    axios
      .get("https://localhost:7051/api/controller/GetAllCategories")
      .then((v) => {
        this.categories = v.data as Category[];
        console.log(this.categories);
      })
      .catch((e) => {});
  }

  public fetchProducts() {
    axios
      .get("https://localhost:7051/api/controller/GetAllProducts")
      .then((v) => {
        this.products = v.data as Product[];
        console.log(this.products);
      })
      .catch((e) => {
        console.error(e);
      });
  }

  public login(email: string, password: string) {
    axios.get(
      "https://localhost:7051/api/controller/Login?username=nameisriaz%40gmail.com&password=huambo%231995"
    );
  }

  public async uploadImage(
    product: Product,
    file: File,
    onComplete: (imageUrl: String, product: Product) => void
  ) {
    try {
      const serviceClient =
        BlobServiceClient.fromConnectionString(connectionString);
      const containerClient = serviceClient.getContainerClient(containerName);
      await containerClient.createIfNotExists();

      const blockBlobClient = containerClient.getBlockBlobClient(
        product.productId
      );
      const uploadOptions = {
        blobHTTPHeaders: {
          blobContentType: file.type || "application/octet-stream", // or determine dynamically
        },
      };

      // await blockBlobClient
      //   .uploadBrowserData(file, uploadOptions)
      //   .then((br) => {
      //     console.log("this is the URL", blockBlobClient.url);
      //   })
      //   .catch((e) => {
      //     console.error(e);
      //   });
    } catch (e) {
      console.error(e);
    }
  }

  public updateProductCategory() {}

  public logout() {
    this.user = null;
  }
}

export const store = new StateManager();
