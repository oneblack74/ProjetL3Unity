using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
	// Retourne la r�uissite de l'int�raction
	public bool Interact()
	{
		return true;
	}
}
