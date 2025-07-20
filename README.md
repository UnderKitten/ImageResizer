# Image Resizer 📸

A lightweight web application that automatically resizes and compresses images to reduce file size while maintaining quality. Built with .NET Web API backend and modern frontend technologies.

## 🎯 Problem \& Solution

**The Problem**: Screenshots and photos are often too large to share online due to platform file size limits and slow upload times.

**The Solution**: A simple drag-and-drop web tool that optimizes images for online sharing while maintaining visual quality, with real-time compression statistics.

## ✨ Features

- 🖼️ **Smart Image Compression** - Preserves aspect ratio and visual quality
- 📏 **Flexible Size Targets** - Pre-configured for common sharing platforms (8MB, 25MB, 50MB) (In Progress)
- 🎨 **Multiple Formats** - Supports JPEG, PNG, GIF, and BMP files
- 📊 **Real-time Feedback** - Shows compression ratios and file size reduction
- 📱 **Responsive Design** - Works on desktop and mobile devices (In progress)


## 🛠️ Tech Stack

**Backend**

- .NET 9 Web API
- ImageSharp for image processing
- ASP.NET Core for RESTful endpoints

**Frontend**

- 

**Deployment**

- 


## 🚀 Quick Start

### Prerequisites

- .NET 8 SDK
- Node.js (if using React/Vue)
- Visual Studio or VS Code


### Installation

**1. Clone the repository**

```bash
git clone https://github.com/UnderKitten/ImageResizer
cd ImageResizer
```

**2. Install backend dependencies**

```bash
dotnet restore
dotnet add package SixLabors.ImageSharp
```

**3. Run the API**

```bash
dotnet run
```

**4. Access Swagger UI**

```
https://localhost:7xxx/swagger
```


### Frontend Setup (Choose One)



## 📚 API Documentation

### Endpoints

| Method | Endpoint | Description |
| :-- | :-- | :-- |
| `POST` | `/api/image/resize` | Resize with custom parameters |
| `POST` | `/api/image/resize/small` | Optimize for 8MB limit |
| `POST` | `/api/image/resize/medium` | Optimize for 25MB limit |
| `POST` | `/api/image/resize/large` | Optimize for 50MB limit |
| `POST` | `/api/image/info` | Get image metadata |


## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🎯 Learning Outcomes

This project teaches:

- [x] File upload handling in web APIs
- [x] Image processing with .NET
- [ ] RESTful API design patterns
- [ ] Modern frontend-backend integration
- [ ] Docker containerization
- [ ] Cloud deployment strategies


## 🔮 Nice to Haves

- [ ] Bulk folder processing
- [ ] Cloud storage integration
- [ ] Mobile app with .NET MAUI

