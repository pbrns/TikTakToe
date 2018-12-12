using System;
namespace TikTakToe.GameManage
{
    public interface IGameObjectManager
    {
        void Initiliaze();
        void AddGameObjectInGame(IGameObject gameObject);
        GameObject LastUsedGameObject { get; }
        Action<bool> WinAction { get; set; }
        void CalculateGameCondition();
    }
}
