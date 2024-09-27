using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.IO;
using System.Threading.Tasks;

class Program
{
    private const string connectionString = "AZURE_BLOB_CONNECTION_STRING"; // Add your Azure Blob Storage connection string here
    private const string containerName = "myimages"; // Your container name
    private const string blobName = "example.txt"; // Name of the blob you want to upload
    private const string filePath = "D:\\\\MS Azure Implementation\\\\blobe_storage_upload_files\\example.txt"; // Path of the file on your local system
    
    
    static async Task Main(string[] args)
    {
        // Upload a Blob
        await UploadBlob();

        // Download the Blob
        await DownloadBlob();
    }

    static async Task UploadBlob()
    {
        // Create a BlobServiceClient object
        BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

        // Create a BlobContainerClient object
        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

        // Get a reference to a BlobClient
        BlobClient blobClient = containerClient.GetBlobClient(blobName);

        // Upload a file to the blob
        using FileStream uploadFileStream = File.OpenRead(filePath);
        await blobClient.UploadAsync(uploadFileStream, true);
        uploadFileStream.Close();

        Console.WriteLine("Upload complete");
    }

    static async Task DownloadBlob()
    {
        // Create a BlobServiceClient object
        BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

        // Create a BlobContainerClient object
        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

        // Get a reference to a BlobClient
        BlobClient blobClient = containerClient.GetBlobClient(blobName);

        // Download the blob
        BlobDownloadInfo download = await blobClient.DownloadAsync();

        // Save it to a file
        string downloadFilePath = "D:\\MS Azure Implementation\\blobe_storage_download_files\\downloaded-example.txt"; // Specify the download path
        using FileStream downloadFileStream = File.OpenWrite(downloadFilePath);
        await download.Content.CopyToAsync(downloadFileStream);
        downloadFileStream.Close();

        Console.WriteLine("Download complete");
    }
}
