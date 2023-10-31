using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gomoku
{
    class Game
    {
        // 管理棋盤狀態。
        private Board board = new Board();

        // 追踪當前下棋的玩家
        private PieceType currentPlayer = PieceType.BLACK;

        // 追踪遊戲的勝利者
        private PieceType winner = PieceType.NONE;

        // 獲取遊戲的勝利者
        public PieceType Winner { get { return winner; } }

        // 檢查是否可以在指定座標放置棋子
        public bool CanBePlaced(int x, int y)
        {
            return board.CanBePlaced(x, y);
        }

        // 在指定座標放置棋子，並檢查是否有玩家獲勝
        public Piece PlaceAPiece(int x, int y)
        {
            Piece piece = board.PlaceAPiece(x, y, currentPlayer);
            if (piece != null)
            {
                // 檢查是否現在下棋的人獲勝
                CheckWinner();

                // 交換選手
                if (currentPlayer == PieceType.BLACK)
                    currentPlayer = PieceType.WHITE;
                else if (currentPlayer == PieceType.WHITE)
                    currentPlayer = PieceType.BLACK;

                return piece;
            }

            return null;
        }

        // 檢查是否有玩家獲勝
        private void CheckWinner()
        {
            int centerX = board.LastPlacedNode.X;
            int centerY = board.LastPlacedNode.Y;

            // 檢查八個不同方向
            for (int xDir = -1; xDir <= 1; xDir++) {
                for (int yDir = -1; yDir <= 1; yDir++) {
                    // 排除中間的情況
                    if (xDir == 0 && yDir == 0)
                        continue;

                    // 紀錄現在看到幾顆相同的棋子
                    int count = 1;
                    while (count < 5)
                    {
                        int targetX = centerX + count * xDir;
                        int targetY = centerY + count * yDir;

                        // 檢查顏色是否相同
                        if (targetX < 0 || targetX >= Board.NODE_COUNT ||
                            targetY < 0 || targetY >= Board.NODE_COUNT ||
                            board.GetPieceType(targetX, targetY) != currentPlayer)
                            break;

                        count++;
                    }

                    // 檢查是否看到五顆棋子
                    if (count == 5)
                        winner = currentPlayer;
                }
            }
        }
    }
}
