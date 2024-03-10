/*
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


[TestFixture]
public class InventoryTests
{
    private Inventory inventory;
    private ItemDefinition kayou;

    [SetUp]
    public void Setup()
    {
        //? Initialise the inventory and a sample stone item 
        inventory = new GameObject().AddComponent<Inventory>();
        kayou = ScriptableObject.CreateInstance<ItemDefinition>();
        kayou.Initialize(2, "Stone", 64);
    }

    [Test]
    public void AddItem_AddsItemToEmptySlot()
    {
        int initialQuantity = inventory.GetSlot(0).GetItemQuantity;

        inventory.AddItem(0, kayou, 3);

        Assert.AreEqual(kayou, inventory.GetSlot(0).GetItem);
        Assert.AreEqual(initialQuantity + 3, inventory.GetSlot(0).GetItemQuantity);
    }

    [Test]
    public void AddItem_AddsItemToMultipleSlots()
    {
        int initialQuantitySlot1 = inventory.GetSlot(0).GetItemQuantity;
        int initialQuantitySlot2 = inventory.GetSlot(1).GetItemQuantity;

        inventory.AddItem(0, kayou, 3);
        inventory.AddItem(1, kayou, 2);

        Assert.AreEqual(kayou, inventory.GetSlot(0).GetItem);
        Assert.AreEqual(kayou, inventory.GetSlot(1).GetItem);
        Assert.AreEqual(initialQuantitySlot1 + 3, inventory.GetSlot(0).GetItemQuantity);
        Assert.AreEqual(initialQuantitySlot2 + 2, inventory.GetSlot(1).GetItemQuantity);
    }

    [Test]
    public void RemoveItem_RemovesItemFromSlot()
    {
        inventory.AddItem(0, kayou, 5);

        (ItemDefinition, int) removedItem = inventory.RemoveItem(0, 3);

        Assert.AreEqual(kayou, removedItem.Item1);
        Assert.AreEqual(3, removedItem.Item2);
        Assert.AreEqual(2, inventory.GetSlot(0).GetItemQuantity);
    }

    [Test]
    public void SwitchItem_SwapsItemsBetweenSlots()
    {
        ItemDefinition otherItem = ScriptableObject.CreateInstance<ItemDefinition>();
        otherItem.Initialize(3, "OtherItem", 10); //! 
        inventory.AddItem(0, kayou, 3);
        inventory.AddItem(1, otherItem, 2);

        inventory.SwitchItem(0, inventory.GetSlot(1));

        Assert.AreEqual(otherItem, inventory.GetSlot(0).GetItem);
        Assert.AreEqual(kayou, inventory.GetSlot(1).GetItem);
        Assert.AreEqual(2, inventory.GetSlot(0).GetItemQuantity);
        Assert.AreEqual(3, inventory.GetSlot(1).GetItemQuantity);
    }

    [TearDown]
    public void TearDown() //? Clean up after each test
    {
        Object.DestroyImmediate(inventory.gameObject);
        Object.DestroyImmediate(kayou);
    }
}
*/