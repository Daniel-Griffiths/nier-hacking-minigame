using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Key {
    public static void Hold(KeyCode key, Action callback) {
        if (IsHolding(key)) callback();
    }

    public static void Down(KeyCode key, Action callback) {
        if (IsDown(key)) callback();
    }

    public static void Up(KeyCode key, Action callback) {
        if (IsUp(key)) callback();
    }

    public static void MouseDown(int key, Action callback) {
        if (IsMouseDown(key)) callback();
    }

    public static bool IsHolding(KeyCode key) {
       return Input.GetKey(key);
    }

    public static bool IsDown(KeyCode key) {
       return Input.GetKeyDown(key);
    }

    public static bool IsUp(KeyCode key) {
       return Input.GetKeyUp(key);
    }

    public static bool IsMouseDown(int key) {
        return Input.GetMouseButton(key);
    }
}