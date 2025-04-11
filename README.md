## 📦 About

This is a simple gRPC-based CRUD service built with .NET Core and Protocol Buffers.  
The project demonstrates how to use Protobuf instead of JSON/XML for data serialization in modern APIs.  
For simplicity, it uses an in-memory database via Entity Framework Core.

---

## 🚀 Usage

Test with Postman (v10+)

Open Postman → New > gRPC Request

Import the .proto file from ./Protos/person.proto

Set server address to: https://localhost:7235

Select a method, e.g. PersonService.CreatePerson

Click on "Use Example Message" to create template message and then customize it
