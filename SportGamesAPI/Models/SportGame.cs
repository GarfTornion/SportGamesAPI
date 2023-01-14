using System;
using System.Collections.Generic;

namespace SportGamesAPI.Models;

public partial class SportGame
{
    public int Id { get; set; }

    public string? Team1Name { get; set; }

    public int? Team1Score { get; set; }

    public string? Team2Name { get; set; }

    public int? Team2Score { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public bool? Finished { get; set; }
}
