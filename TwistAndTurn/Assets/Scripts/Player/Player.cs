using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IMoveable
{
	public enum Rotation
	{
		Left,
		Right,
		None,
	}
	
	static public Player instance;
	private float moveSpeed = 0.05f;
	private float stepPauseDuration = 0.001f;
	private float elapsedTime;
	public Vector3 moveDirection;
	public Vector3 startPosition;
	private Vector3 endPosition;
	public bool canMove;
	private Vector3 headPosition;
	public List<Transform> children = new List<Transform>();
	public List<Vector3> boxStartPositions = new List<Vector3>();
	private IPlaceable headTile;
	private IMoveable headMoveable;
	public Rotation rotation;

	
	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
		
	}
	
	void Start()
	{
		//StartCoroutine(Move());
	}

	void OnEnable()
	{
		StartCoroutine(WaitForGridSystemAndStartMove());
	}

	private IEnumerator WaitForGridSystemAndStartMove()
	{
		// Wait until GridSystem.instance is not null
		yield return new WaitUntil(() => GridSystem.instance != null);
		StartCoroutine(Move());
	}



	private IEnumerator Move()
	{
		while (true)
		{
			if(!GridSystem.instance.isGameStart)
			{
				yield return null;
				continue;
			}
			if (transform.position == GridSystem.instance.GetGridPosition(transform.position))
			{
				rotation = Rotation.None;
				startPosition = transform.position;
				endPosition = startPosition + moveDirection;
				elapsedTime = 0;
				for(int i = 0; i <= children.Count; i++)
				{
					headPosition = startPosition + ((i + 1) * moveDirection);
					headTile = GridSystem.instance.levelMatrix[(int)headPosition.x, (int)headPosition.y];
					headMoveable = GridSystem.instance.moveables[(int)headPosition.x, (int)headPosition.y];
					if (headTile != null)
					{
						headTile.ApplyEffect(this);
					}
					if (headMoveable != null)
					{
						((IPlaceable)headMoveable).ApplyEffect(this);
					}
				}
				yield return new WaitForSeconds(stepPauseDuration);
			}
			if (canMove)
			{
				GetChildMoveablesPosition();
				while (elapsedTime < moveSpeed && GridSystem.instance.isGameStart)
				{
					elapsedTime += Time.deltaTime;
					transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / moveSpeed);
					for (int i = 0; i < children.Count; i++)
					{
						Box box = children[i].GetComponent<Box>();
						box.MoveWithPlayer(elapsedTime, moveSpeed, moveDirection, boxStartPositions[i]);
					}
					yield return null;
				}
				UpdateChildMoveablesPosition();
				CheckRotation();
				yield return new WaitForSeconds(moveSpeed);
			}
			else
			{
				yield return null;
			}
		}
	}

	public void RotateLeft()
	{
		StartCoroutine(RotateLeftCoroutine());
	}

	public void RotateRight()
	{
		StartCoroutine(RotateRightCoroutine());
	}

	private IEnumerator RotateLeftCoroutine()
	{
		DetachAllChildBoxes();
		yield return new WaitForSeconds(stepPauseDuration*0.5f);
		moveDirection = new Vector2(-moveDirection.y, moveDirection.x);
		transform.Rotate(0, 0, 90); 
	}

	private IEnumerator RotateRightCoroutine()
	{
		DetachAllChildBoxes();
		yield return new WaitForSeconds(stepPauseDuration * 0.5f);
		moveDirection = new Vector2(moveDirection.y, -moveDirection.x);
		transform.Rotate(0, 0, -90); 
	}

	public void DetachAllChildBoxes()
	{
		children.Clear();
	}
	
	private void GetChildMoveablesPosition()
	{
		boxStartPositions.Clear();
		for (int i = 0; i < children.Count; i++)
		{
			boxStartPositions.Add(children[i].position);
		}
	}

	private void UpdateChildMoveablesPosition()
	{
		boxStartPositions.Clear();
		for (int i = children.Count - 1; i >= 0; i--)
		{
			boxStartPositions.Insert(0, children[i].position);
			GridSystem.instance.UpdateMoveables(children[i].position - moveDirection, children[i].position);
		}
	}
	
	private void CheckRotation()
	{
		switch (rotation)
		{
			case Rotation.Left:
				RotateLeft();
				break;
			case Rotation.Right:
				RotateRight();
				break;
			case Rotation.None:
				break;
		}
	}
	
	public void ResetPlayer(Vector3 playerStartCoordinate, Vector3 playerStartDirection)
	{
		DetachAllChildBoxes();
		transform.position = playerStartCoordinate;
		moveDirection = playerStartDirection;
		if (moveDirection == Vector3.right)
		{
			transform.rotation = Quaternion.Euler(0, 0, 0);
		}
		else if(moveDirection == Vector3.left)
		{
			transform.rotation = Quaternion.Euler(0, 0, 180);
		}
		else if(moveDirection == Vector3.up)
		{
			transform.rotation = Quaternion.Euler(0, 0, 90);
		}
		else if(moveDirection == Vector3.down)
		{
			transform.rotation = Quaternion.Euler(0, 0, -90);
		}
		canMove = true;
	}
}