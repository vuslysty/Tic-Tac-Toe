using System.Threading.Tasks;
using Enums;

namespace Logic
{
    public interface IPlayer
    {
        Task<CellPosition> GetRightToMove();
        void Cleanup();
        Figure Figure { get; }
    }
}