# 🌌 NebulaOps

**NebulaOps** is an open-source, cross-platform infrastructure dashboard built with .NET MAUI Blazor Hybrid. It simplifies server and container management through SSH automation, real-time metrics, and intuitive UI — all self-hosted, with no cloud lock-in.

---

## 🚀 Features

- 🔐 Connect to remote servers via SSH
- 🧠 Execute terminal commands interactively
- 📦 Manage Docker/Podman containers (start, stop, restart, logs)
- 📊 View real-time host metrics (CPU, memory, disk, network)
- 📈 Stream container logs and stats
- 🧭 Responsive dashboard with MudBlazor components
- 🛠️ Fully offline, self-hosted, and open-source

---

## 🛠️ Tech Stack

| Layer         | Technology                     |
|--------------|---------------------------------|
| UI           | .NET MAUI Blazor Hybrid + MudBlazor |
| Backend      | ASP.NET Core + SignalR          |
| SSH          | SSH.NET (`Renci.SshNet`)        |
| Containers   | Docker.DotNet / Podman CLI      |
| Charts       | MudBlazor Charts / Chart.js     |
| Storage      | PostgreSQL / SQLite (planned)   |

---

## 📦 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- Visual Studio 2022+ or VS Code
- Git

### Clone the repo

```bash
git clone https://github.com/yourusername/NebulaOps.git
cd NebulaOps
```
Run the app

```bash
dotnet build
dotnet run --project src/NebulaOps.UI.Web.Client
```

For MAUI Hybrid builds, open NebulaOps.App in Visual Studio and run on your target platform.


📁 Project Structure
NebulaOps/
├── src/
│   ├── NebulaOps.App/           # MAUI Hybrid app
│   ├── NebulaOps.UI.Web/        # Web backend (SignalR, API)
│   ├── NebulaOps.UI.Web.Client/ # Blazor Web client
│   ├── NebulaOps.UI.Shared/     # Shared UI components/pages
│   └── NebulaOps.Core/          # SSH, Docker, metrics logic
├── docs/                        # Documentation
├── tests/                       # Unit and integration tests
└── README.md



🧪 Status
NebulaOps is in early development. Current focus:
- [x] SSH terminal component
- [x] Dashboard with fake metrics
- [ ] Container lifecycle management
- [ ] Real-time metrics via SignalR
- [ ] Host registry and credentials vault

🤝 Contributing
We welcome contributions! Whether it's bug fixes, new features, or ideas — feel free to fork, submit PRs, or open issues.
To contribute:
- Fork the repository
- Create a new branch (git checkout -b feature/my-feature)
- Commit your changes
- Push and open a pull request

📜 License
This project is licensed under the MIT License. See the LICENSE file for details.

🌐 Links
- Project Website (coming soon)
- Documentation
- Issues

Made with 💻 and ☕ by starlight
