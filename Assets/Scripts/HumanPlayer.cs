﻿using System.Collections;

public class HumanPlayer : Player {

	public static Move[] legalMovesInPosition;

	public override void Init (bool white) {
		base.Init (white);

		if (white) {
			MoveGenerator3 move2 = new MoveGenerator3();
			Move[] moves = move2.GetAllLegalMoves(currentPosition);
			for (int i = 0; i < moves.Length; i ++) {
				//UnityEngine.Debug.Log(moves[i].algebraicMove);
			}
		}

	}

	/// <summary>
	/// Make a move that is known to be legal
	/// </summary>
	protected override void MakeMove (Move move)
	{
		base.MakeMove (move);
		legalMovesInPosition = moveGenerator.GetAllLegalMoves (currentPosition);
	}

	/// <summary>
	/// Makes the move after confirming that it is legal
	/// </summary>
	public void TryMakeMove(string algebraicMove) {
		string fromSquareAlgebraic = algebraicMove [0].ToString() + algebraicMove [1].ToString();
		Coord fromSquare = new Coord (fromSquareAlgebraic);

		// abort legality check if trying to move wrong colour piece
		if (!currentPosition.AllPieces(isWhite).ContainsPieceAtSquare (fromSquare)) { 
			return;
		}

		for (int i = 0; i < legalMovesInPosition.Length; i ++) {
			string moveCoords = legalMovesInPosition [i].algebraicMove.Substring(0,4); // cut off pawn promotion
			if (moveCoords == algebraicMove) { // move confirmed as legal
				MakeMove(legalMovesInPosition[i]);
			}
		}
	}

	public override void RequestMove ()
	{
		base.RequestMove ();
		legalMovesInPosition = moveGenerator.GetAllLegalMoves (currentPosition);
	}

}
