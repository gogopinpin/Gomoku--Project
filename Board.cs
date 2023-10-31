﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Gomoku
{
    class Board
    {
        // 棋盤的節點數量
        public static readonly int NODE_COUNT = 9;

        // 無匹配節點的狀態
        private static readonly Point NO_MATCH_NODE = new Point(-1, -1);

        // 棋盤的偏移量
        private static readonly int OFFSET = 75;
        // 節點的半徑
        private static readonly int NODE_RADIUS = 10;
        // 節點之間的距離
        private static readonly int NODE_DISTANCE = 75;

        // 建立一個二維陣列 pieces，用於表示棋盤上各個節點的棋子狀態
        private Piece[,] pieces = new Piece[NODE_COUNT, NODE_COUNT];

        // 最後一次放置棋子的節點位置
        private Point lastPlacedNode = NO_MATCH_NODE;
        public Point LastPlacedNode { get { return lastPlacedNode; } }

        // 獲取指定節點位置的棋子類型
        public PieceType GetPieceType(int nodeIdX, int nodeIdY)
        {
            if (pieces[nodeIdX, nodeIdY] == null)
                return PieceType.NONE;
            else
                return pieces[nodeIdX, nodeIdY].GetPieceType();
        }
        // 檢查是否可以在指定的座標放置棋子
        public bool CanBePlaced(int x, int y)
        {
            // 找出最近的節點 (Node)
            Point nodeId = findTheClosetNode(x, y);

            // 如果沒有的話，回傳 false
            if (nodeId == NO_MATCH_NODE)
                return false;

            // 如果有的話，檢查是否已經棋子存在
            if (pieces[nodeId.X, nodeId.Y] != null)
                return false;

            return true;
        }

        // 在指定座標放置棋子
        public Piece PlaceAPiece(int x, int y, PieceType type)
        {
            // 找出最近的節點 (Node)
            Point nodeId = findTheClosetNode(x, y);

            // 如果沒有的話，回傳 false
            if (nodeId == NO_MATCH_NODE)
                return null;

            // 如果有的話，檢查是否已經棋子存在
            if (pieces[nodeId.X, nodeId.Y] != null)
                return null;

            // 根據 type 產生對應的棋子
            Point formPos = convertToFormPosition(nodeId);
            if (type == PieceType.BLACK)
                pieces[nodeId.X, nodeId.Y] = new BlackPiece(formPos.X, formPos.Y);
            else if (type == PieceType.WHITE)
                pieces[nodeId.X, nodeId.Y] = new WhitePiece(formPos.X, formPos.Y);

            // 紀錄最後下棋子的位置
            lastPlacedNode = nodeId;

            return pieces[nodeId.X, nodeId.Y];
        }

        // 將節點位置轉換為視窗上的座標
        private Point convertToFormPosition(Point nodeId)
        {
            Point formPosition = new Point();
            formPosition.X = nodeId.X * NODE_DISTANCE + OFFSET;
            formPosition.Y = nodeId.Y * NODE_DISTANCE + OFFSET;
            return formPosition;
        }

        // 找到最接近指定座標的節點位置
        private Point findTheClosetNode(int x, int y)
        {
            int nodeIdX = findTheClosetNode(x);
            if (nodeIdX == -1 || nodeIdX >= NODE_COUNT)
                return NO_MATCH_NODE;

            int nodeIdY = findTheClosetNode(y);
            if (nodeIdY == -1 || nodeIdY >= NODE_COUNT)
                return NO_MATCH_NODE;

            return new Point(nodeIdX, nodeIdY);
        }

        // 找到最接近指定位置的節點位置
        private int findTheClosetNode(int pos)
        {
            if (pos < OFFSET - NODE_RADIUS)
                return -1;

            pos -= OFFSET;

            int quotient = pos / NODE_DISTANCE;
            int remainder = pos % NODE_DISTANCE;

            if (remainder <= NODE_RADIUS)
                return quotient;
            else if (remainder >= NODE_DISTANCE - NODE_RADIUS)
                return quotient + 1;
            else
                return -1;
        }
    }
}