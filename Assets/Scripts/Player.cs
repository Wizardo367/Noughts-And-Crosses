using System;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    // External variables
    [SerializeField] private Board _board;
    [SerializeField] private Manager _manager;

    // Variables
    public bool Automated;

    public Marker Marker;

    public int Score
    {
        get { return Marker == Marker.Cross ? _manager.XScore : _manager.OScore; }
        set
        {
            if (Marker == Marker.Cross)
                _manager.XScore = value;
            else
                _manager.OScore = value;
        }
    }

    public void AutoMove()
    {
        // Get copy of spaces from board
        Marker[] originalSpaces = _board.Spaces;

        // Check if the middle space is occupied
        if (originalSpaces[4] == Marker.None)
        {
            _board.PlaceMarker(4);
            return;
        }

        // Find the best move
        int bestValue = -1000;
        int bestMove = -1;

        // Traverse all empty spaces, calling minimax on all of them.
        for (int i = 0; i < 9; i++)
        {
            // Check if the space is empty
            if (originalSpaces[i] == Marker.None)
            {
                // Make the move
                originalSpaces[i] = Marker;
                // Check output
                int moveValue = Minimax(originalSpaces, 0, false);
                // Undo move
                originalSpaces[i] = Marker.None;

                // Check if the current move is better than the current
                if (moveValue > bestValue)
                {
                    bestMove = i;
                    bestValue = moveValue;
                }
            }
        }

        // Make best move
        if (bestMove >= 0 && bestMove <= 9)
            _board.PlaceMarker(bestMove);
    }

    private int Minimax(Marker[] spaces, int depth, bool isMax)
    {
        // Get the other player's marker
        Marker otherPlayer = Marker == Marker.Cross ? Marker.Nought : Marker.Cross;

        // Get score (10 = maximiser has won, -10 = minimiser has won)
        GameState gameState = Board.Process(spaces, Marker);

        // Return score if there is a winner or tie (The numbers are the score)
        switch (gameState)
        {
            case GameState.Win:
                return 10 - depth;
            case GameState.Lose:
                return -10 + depth;
            case GameState.Tie:
                return 0;
        }

        // Get max or min score
        int bestScore = isMax ? -1000 : 1000;

        // Traverse all empty spaces
        for (int i = 0; i < 9; i++)
        {
            // Check if the space is empty
            if (spaces[i] == Marker.None)
            {
                // Make the move
                spaces[i] = isMax ? Marker : otherPlayer;
                // Execute the minimax function recursively
                bestScore = isMax
                    ? Math.Max(bestScore, Minimax(spaces, depth + 1, !isMax))
                    : Math.Min(bestScore, Minimax(spaces, depth + 1, !isMax));
                // Undo the move
                spaces[i] = Marker.None;
            }
        }

        return bestScore;
    }
}