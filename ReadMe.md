# HTMLToDocxConverterDemo

This project is an Azure Function that converts an uploaded HTML file into a DOCX file and returns it as a downloadable response. Below are the steps to test the API via a terminal and instructions for calling this function from another service.

---

## **Testing the API via Terminal**

### Prerequisites
1. Ensure the Azure Function is deployed and running. You can test it locally or on Azure.
2. Note the API endpoint:
   - **Local**: `http://localhost:7071/api/TestTrigger`
   - **Azure**: `https://myhelloworldtest.azurewebsites.net/api/TestTrigger`
3. Have an HTML file ready for testing (e.g., `test.html`).

### Steps to Test
1. Open a terminal.
2. Use the `curl` command to send a POST request with the HTML file:
   ```bash
   curl -X POST \
     -F "file=@/path/to/test.html" \
     http://localhost:7071/api/TestTrigger