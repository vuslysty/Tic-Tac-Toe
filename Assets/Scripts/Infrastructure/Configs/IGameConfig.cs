using Enums;
using Infrastructure.Services;

namespace Infrastructure.Configs
{
    public interface IGameConfig : IService
    {
        int Rows { get; set; }
        int Cols { get; set; }
        GameMode GameMode { get; set; }
        BotType BotType { get; set; }
        int GetWinLenght();
    }
}