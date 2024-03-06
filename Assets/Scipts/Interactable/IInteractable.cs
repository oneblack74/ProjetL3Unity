using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
	// Retourne la réuissite de l'intéraction
    public bool Interact()
	{
		return true;
	}
}
