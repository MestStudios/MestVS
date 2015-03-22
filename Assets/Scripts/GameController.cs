using UnityEngine;
using UnityEngine.UI;
using System.Collections;

enum State {
	IDLE,
	PLAYER1,
	PLAYER2,
	RESOLUTION,
	GAMEOVER
}

public class GameController : MonoBehaviour {
	public PlayerController player1, player2;
	private State state;
	int player1Action, player2Action;
	public Text actualPlayerText;
	public void Start() {
		state = State.IDLE;
		actualPlayerText.text = "ESPERANDO";
	}

	public void Update() {
		switch (state) {
		case State.IDLE:
			actualPlayerText.text = "ESPERANDO";
			state = State.PLAYER1;
			break;
		case State.PLAYER1:
			actualPlayerText.text = "Player 1";
			break;
		case State.PLAYER2:
			actualPlayerText.text = "Player 2";
			break;
		case State.RESOLUTION:
			actualPlayerText.text = "Resolving Turn";
			payPlayerStaminaCost(0, player1Action);
			payPlayerStaminaCost(1, player2Action);
			if (player1Action > 0 && player2Action > 0) {
				player1.applyDamage(player2Action);
				player2.applyDamage(player1Action);
			} else if (player1Action < 0 && player2Action > 0) {
				player1.applyDamage(player2Action + player1Action);
			} else if (player1Action > 0 && player2Action < 0) {
				player2.applyDamage(player1Action + player2Action);
			}
			if (player1.isDead() || player2.isDead()) {
				state = State.GAMEOVER;
			} else {
				player1.regainStamina();
				player2.regainStamina();
				state = State.IDLE;
			}
			break;
		case State.GAMEOVER:
			actualPlayerText.text = "GAME OVER";
			break;
		default:
			break;
		}
	}

	private bool canPlayerDoAction(int player, int action) {
		if (action < 0) {
			action = -action - 1;
		}
		if (player == 0) {
			return player1.canPayStamina(action);
		} else {
			return player2.canPayStamina(action);
		}
	}

	private void payPlayerStaminaCost(int player, int action) {
		if (action < 0) {
			action = -action -1;
		}
		if (player == 0) {
			player1.payStamina (action);
		} else {
			player2.payStamina(action);
		}
	}

	public void setPlayerAction(int action) {
		if (state == State.PLAYER1) {
			if (canPlayerDoAction(0, action)) {
				setPlayer1Action(action);
				state = State.PLAYER2;
			}
		} else if (state == State.PLAYER2) {
			if (canPlayerDoAction(1, action)) {
				setPlayer2Action(action);
				state = State.RESOLUTION;
			}
		}
	}

	private void setPlayer1Action(int action) {
		player1Action = action;
	}

	private void setPlayer2Action(int action) {
		player2Action = action;
	}
}
