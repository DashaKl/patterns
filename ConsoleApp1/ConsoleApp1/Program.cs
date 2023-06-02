using System;
using System.Collections.Generic;

interface IGamePiece
{
    void Display();
}
class Soldier : IGamePiece
{
    public void Display()
    {
        Console.WriteLine("Soldier");
    }
}

class Archer : IGamePiece
{
    public void Display()
    {
        Console.WriteLine("Archer");
    }
}

class Wizard : IGamePiece
{
    public void Display()
    {
        Console.WriteLine("Wizard");
    }
}

class Team
{
    private List<IGamePiece> team;

    public Team()
    {
        team = new List<IGamePiece>();
    }

    public void AddGamePiece(IGamePiece gamePiece)
    {
        team.Add(gamePiece);
    }

    public void DisplayTeam()
    {
        foreach (var gamePiece in team)
        {
            gamePiece.Display();
        }
    }

    public TeamMemento SaveTeam()
    {
        return new TeamMemento(team);
    }

    public void RestoreTeam(TeamMemento memento)
    {
        team = memento.GetState();
    }
}

// Хранитель для сохранения и восстановления состояния команды игровых фигур
class TeamMemento
{
    private List<IGamePiece> state;

    public TeamMemento(List<IGamePiece> team)
    {
        state = new List<IGamePiece>(team);
    }

    public List<IGamePiece> GetState()
    {
        return new List<IGamePiece>(state);
    }
}

// Фасад для сохранения и восстановления команды игровых фигур с использованием хранителя
class TeamFacade
{
    private Team team;
    private TeamMemento memento;

    public TeamFacade()
    {
        team = new Team();
    }

    public void AddSoldier()
    {
        team.AddGamePiece(new Soldier());
    }

    public void AddArcher()
    {
        team.AddGamePiece(new Archer());
    }

    public void AddWizard()
    {
        team.AddGamePiece(new Wizard());
    }

    public void SaveTeam()
    {
        memento = team.SaveTeam();
    }

    public void RestoreTeam()
    {
        team.RestoreTeam(memento);
    }

    public void DisplayTeam()
    {
        team.DisplayTeam();
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Создаем команду игровых фигур с помощью фасада
        TeamFacade teamFacade = new TeamFacade();
        teamFacade.AddSoldier();
        teamFacade.AddArcher();
        teamFacade.AddWizard();

        // Сохраняем состояние команды игровых фигур
        teamFacade.SaveTeam();

        // Изменяем состав команды
        teamFacade.AddSoldier();

        // Выводим информацию о составе команды
        Console.WriteLine("Updated Team:");
        teamFacade.DisplayTeam();

        // Восстанавливаем состояние команды игровых фигур
        teamFacade.RestoreTeam();

        // Выводим восстановленную информацию о составе команды
        Console.WriteLine("\nRestored Team:");
        teamFacade.DisplayTeam();

        Console.ReadKey();
    }
    
}
