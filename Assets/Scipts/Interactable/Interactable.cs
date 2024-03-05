using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
	[SerializeField] private IInteractable interactableComponent;

	public void Interact()
	{
		interactableComponent.Interact();
	}
}
