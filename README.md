# AsukaNet-Api (Server)

Client repo: [Link to Discord bot client](https://github.com/blai30/AsukaNet)

ASP.NET Web API backend for AsukaNet Discord bot written in C# and .NET.


## Tech Stack

**Server:** ASP.NET Core, MySQL, Entity Framework Core, MediatR

Note: The database schema must exist before database migrations can execute.


## Features

- C# 10 + .NET 6
- Dependency Injection
- Database Migrations
- Authorization (not implemented)
- Docker
- Cross platform


## Endpoints

### ReactionRoles

`GET /api/reactionroles`

`GET /api/reactionroles/{id}`

| Parameter | Type   | Description                                  |
|-----------|--------|----------------------------------------------|
| id        | number | The ID of the reaction role in the database. |

`POST /api/reactionroles`

| Parameter  | Type   | Description                                                                    |
|------------|--------|--------------------------------------------------------------------------------|
| guild_id   | string | The guild's ID.                                                                |
| channel_id | string | The guild text channel's ID.                                                   |
| message_id | string | The guild message's ID.                                                        |
| role_id    | number | The guild role's ID.                                                           |
| reaction   | number | A Unicode emoji or the string representation of a custom Discord server emote. |

`DELETE /api/reactionroles`

| Parameter  | Type   | Description                                                                    |
|------------|--------|--------------------------------------------------------------------------------|
| message_id | string | The guild message's ID.                                                        |
| role_id    | number | The guild role's ID.                                                           |
| reaction   | number | A Unicode emoji or the string representation of a custom Discord server emote. |

`DELETE /api/reactionroles/{id}`

| Parameter | Type   | Description                                  |
|-----------|--------|----------------------------------------------|
| id        | number | The ID of the reaction role in the database. |

### Tags

`GET /api/tags`

`GET /api/tags/{id}`

| Parameter | Type   | Description                        |
|-----------|--------|------------------------------------|
| id        | number | The ID of the tag in the database. |

`POST /api/tags`

| Parameter | Type   | Description                                                                    |
|-----------|--------|--------------------------------------------------------------------------------|
| name      | string | Name or call of the tag.                                                       |
| content   | string | Content or response of the tag.                                                |
| reaction  | string | A Unicode emoji or the string representation of a custom Discord server emote. |
| guild_id  | number | The guild's ID.                                                                |
| user_id   | number | The user's ID.                                                                 |

`DELETE /api/tags/{id}`

| Parameter | Type   | Description                        |
|-----------|--------|------------------------------------|
| id        | number | The ID of the tag in the database. |

`PUT /api/tags`

| Parameter | Type   | Description                                                                    |
|-----------|--------|--------------------------------------------------------------------------------|
| id        | number | The ID of the tag in the database.                                             |
| content   | string | Content or response of the tag.                                                |
| reaction  | string | A Unicode emoji or the string representation of a custom Discord server emote. |

## Run Locally

Clone the project

```bash
git clone https://github.com/blai30/AsukaNet-Api.git
```

Go to the project directory

```bash
cd AsukaNet-Api
```

Start the server (will install NuGet dependencies)

```bash
dotnet run --project src/WebApi
```
