using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type
{
    Lunch,
    Recommend,
    Hamburger,
    Dessert,
    SetDessert,
    SetDrink
}

[Serializable]
public class Menu
{
    public string Name;
    public int Price;
    public Sprite Image;
    public Type[] Type;
}
