# Google Calendar API Documentation

## Introduction

This documentation provides a step-by-step guide to set up and use a .NET Core API application that facilitates users in managing their Google Calendar events. The application includes endpoints to create, view, and delete events in the user's Google Calendar.

## Technologie and Design
C#, OOP, DI, N-tiers, ASP.Net web API.

### Packages
Google.Apis.Calendar.v3
AutoMapper.Extensions.Microsoft.DependencyInjection
Swashbuckle.AspNetCore

## Project Setup

Follow the steps below to set up the project:

1. Set up a .NET Core (Version 6 or 7) API project.
2. Install the necessary NuGet package, `Google.Apis.Calendar.v3`, to interact with the Google Calendar API.
3. Create a project in the Google Developer Console.
4. Enable the Google Calendar API for your project.
5. Configure OAuth 2.0 credentials, including the necessary redirect URIs.

## API Endpoints

The API provides the following endpoints to interact with the Google Calendar:

### 1. Create Event

Endpoint: `POST /api/events`

Functionality: This endpoint allows users to create a new event in their Google Calendar.

Input: Accept a JSON object containing the event details.

Output: Return the details of the created event along with a `201 Created` HTTP status code.

#### Details:

- Validate the input to ensure all required fields are present and formatted correctly.
- Use the Google Calendar API to create an event in the user's calendar.
- Handle any errors that might occur during the API call, such as authentication issues or invalid input.
- Users cannot create events in the past or on Fridays or Saturdays.

### 2. View Events

Endpoint: `GET /api/events`

Functionality: Retrieve a list of events from the user's Google Calendar.

Input: Optional query parameters for filtering (e.g., date range, search query).

Output: Return a list of events matching the filter criteria.

#### Details:

- Translate any provided filters into the appropriate format for the Google Calendar API.
- Handle pagination if the number of events is large.

### 3. Delete Event

Endpoint: `DELETE /api/events/{eventId}`

Functionality: Remove an event from the user’s Google Calendar.

Input: The `eventId` as a URL parameter.

Output: Return a `204 No Content` HTTP status code upon successful deletion.

#### Details:

- Validate the `eventId`.
- Use the Google Calendar API to delete the event.
- Handle errors such as the event not being found or issues with the API call.

## Running the Application

To run the application, follow these steps:

1. Clone the repository from GitHub.
2. Set up the necessary Google API credentials by following the instructions provided in the project setup section.
3. Configure the application settings, such as the Google API credentials and any other required settings.
4. Build and run the application using the .NET CLI or an integrated development environment (IDE) of your choice.

Please note that additional instructions specific to your project may be included in the project's README file or other relevant documentation.

## Conclusion

This documentation provides an overview of the .NET Core API application for managing Google Calendar events. It includes detailed information on project setup, API endpoints, and running the application.