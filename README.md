# ExceptionHandlerPOC

A proof-of-concept ASP.NET Core Web API project demonstrating a robust global exception handling mechanism using .NET 8. This project showcases how to implement a custom global exception handler that returns standardized error responses using `ProblemDetails`, adhering to [RFC 7807](https://tools.ietf.org/html/rfc7807).

## Table of Contents

- [Introduction](#introduction)
- [Features](#features)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
  - 
## Introduction

ExceptionHandlerPOC is designed to demonstrate a robust global exception handling mechanism in an ASP.NET Core Web API using .NET 8. This project focuses on:

- Centralized exception handling.
- Standardized error responses using `ProblemDetails`.
- Handling specific exception types differently.
- Including detailed error information in development environments.
- Logging errors with correlation IDs for easier debugging.

## Features

- **Global Exception Handling**: Centralized handling of exceptions across the application.
- **Standardized Error Responses**: Uses `ProblemDetails` for consistent error responses following [RFC 7807](https://tools.ietf.org/html/rfc7807).
- **Custom Exception Types**: Demonstrates handling of custom exceptions like `NotFoundException` and `ValidationException`.
- **Environment-Specific Details**: Detailed error messages in development, generic messages in production.
- **Correlation IDs**: Adds a `traceId` to error responses and logs for tracing.
- **Extensible**: Easily add new exception types and customize responses.

## Getting Started

### Prerequisites

- **.NET 8 SDK**: Ensure you have .NET 8 installed on your machine.
  - [Download .NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- **IDE**: Visual Studio 2022 (17.4 or later), Visual Studio Code, or any other C# IDE.
- **HTTPS Development Certificate**: Trust the ASP.NET Core HTTPS development certificate.

### Installation

1. **Clone the Repository**

   ```bash
   git clone https://github.com/yourusername/ExceptionHandlerPOC.git
