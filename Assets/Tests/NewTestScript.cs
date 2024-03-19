using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class NewTestScript
{
    // A Test behaves as an ordinary method
    [Test]
    public void NewTestScriptSimplePasses()
    {
        // GameObject gm = GameObject.Instantiate(new GameObject("GameManager"));
        // GameManager manager = gm.AddComponent<GameManager>();

        // GameObject player = GameObject.Instantiate(new GameObject("Player"));
        // Inventory inv = player.AddComponent<Inventory>();

        // inv.AddItemFast(manager.ConvertIdToItem(2), 1);

        //Debug.Log(inv.CheckItemQuantity(0));

        Debug.Assert(1 == 1);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator NewTestScriptWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
