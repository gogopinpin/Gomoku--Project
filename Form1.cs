using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gomoku
{
    //建立應用程式的主視窗
    public partial class Form1 : Form
    {
        // 管理遊戲的邏輯
        private Game game = new Game();

        // 初始化視窗元件
        public Form1()
        {
            InitializeComponent();
        }

        // 根據滑鼠座標判斷是否可以在該位置放置棋子，並設定游標的顯示。
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            Piece piece = game.PlaceAPiece(e.X, e.Y);
            if (piece != null)
            {
                this.Controls.Add(piece);

                // 檢查是否有人獲勝
                if (game.Winner == PieceType.BLACK)
                {
                    MessageBox.Show("黑色獲勝");
                } else if (game.Winner == PieceType.WHITE)
                {
                    MessageBox.Show("白色獲勝");
                }
            }
        }

        
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (game.CanBePlaced(e.X, e.Y))
            {
                this.Cursor = Cursors.Hand;
            } else
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}
