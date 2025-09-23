using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NebulaOps.Context.UI.Entity;

public class Agent
{
    public Guid Id { get; set; }

    public string Hostname { get; set; } = string.Empty;

    public string IpAddress { get; set; } = string.Empty;

    public int Port { get; set; }

    public string Username { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string? SshKey { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
}
