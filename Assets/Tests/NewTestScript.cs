using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class NewTestScript
{
    GameManager manager;
    Inventory inv;
    Inventory inv_cursor;
    GameObject cursor;

    [SetUp]
    public void Init()
    {
        GameObject gm = GameObject.Instantiate(new GameObject("GameManager"));
        manager = gm.AddComponent<GameManager>();

        manager.LoadItems();

        GameObject player = GameObject.Instantiate(new GameObject("Player"));
        inv = player.AddComponent<Inventory>();

        inv.InitManager(manager);
        inv.InitSlots();

        cursor = GameObject.Instantiate(new GameObject("Cursor"));
        inv_cursor = cursor.AddComponent<Inventory>();
        cursor.AddComponent<Cursor>();

        inv_cursor.InitManager(manager);
        inv_cursor.InitSlots();
    }

    [Test]
    public void AddFastTest()
    {

        inv.AddItemFast(manager.ConvertIdToItem(1), 2);

        Debug.Assert(inv.CheckItemQuantity(0) == 2);
    }

    [Test]
    public void AddItemTest()
    {

        inv.AddItem(1, manager.ConvertIdToItem(2), 1);

        Debug.Assert(inv.CheckItem(1).GetID == 2);
    }

    [Test]
    public void RemoveItem()
    {

        inv.AddItem(1, manager.ConvertIdToItem(2), 1);

        inv.RemoveItem(1, 1);

        Debug.Assert(inv.CheckItem(1).GetID != 2);
    }

    [Test]
    public void SwapItem()
    {

        inv.AddItem(0, manager.ConvertIdToItem(1), 1);

        inv.SwitchItem(0, inv.GetSlot(1));

        Debug.Assert(inv.CheckItem(1).GetID == 1);
    }

    [Test]
    public void AddItemFastQuandPlein()
    {


        inv.Fill();

        (ItemDefinition, int) res = inv.AddItemFast(manager.ConvertIdToItem(2), 5);

        Debug.Assert(res.Item2 == 5);
    }

    [Test]
    public void AddItemSlotQuandPlein()
    {


        inv.Fill();
        try
        {
            inv.AddItem(0, manager.ConvertIdToItem(0), 1);
            Debug.Assert(false);
        }
        catch (Exception)
        {
            Debug.Assert(true);
        }
    }

    [Test]
    public void PrendreItemSlotVide()
    {


        try
        {
            inv.RemoveItem(0, 1);
            Debug.Assert(false);
        }
        catch (Exception)
        {
            Debug.Assert(true);
        }
    }

    [Test]
    public void RightClickVide()
    {


        cursor.GetComponent<Inventory>().AddItemFast(manager.ConvertIdToItem(1), 1);

        inv.RightCLick(0, cursor);

        Debug.Assert(inv.CheckItem(0).GetID == 1);
    }

    [Test]
    public void RightClickPrendrePair()
    {


        inv.AddItemFast(manager.ConvertIdToItem(1), 10);

        inv.RightCLick(0, cursor);

        Debug.Assert(cursor.GetComponent<Inventory>().CheckItemQuantity(0) == 5);
    }

    [Test]
    public void RightClickPrendreImpair()
    {


        inv.AddItemFast(manager.ConvertIdToItem(1), 9);

        inv.RightCLick(0, cursor);

        Debug.Assert(cursor.GetComponent<Inventory>().CheckItemQuantity(0) == 5);
    }

    [Test]
    public void RightClickPrendreVide()
    {


        inv.RightCLick(0, cursor);

        Debug.Assert(cursor.GetComponent<Inventory>().CheckItemQuantity(0) == 0);
    }

    [Test]
    public void AddItemFastOverflow()
    {


        inv.AddItemFast(manager.ConvertIdToItem(1), manager.ConvertIdToItem(1).GetMaxStack - 2);

        inv.AddItemFast(manager.ConvertIdToItem(1), 5);

        Debug.Assert(inv.CheckItemQuantity(1) == 3);
    }
}
