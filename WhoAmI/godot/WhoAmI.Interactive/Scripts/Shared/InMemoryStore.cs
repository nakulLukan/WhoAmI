using System;
using System.Collections.Generic;
using Godot;

public static class InMemoryStore
{
    public static IDictionary<string, Spatial> GameObjects = new Dictionary<string, Spatial>();

    public static void AddGameObject(string tagName, Spatial gameObject){
        if(GameObjects.ContainsKey(tagName)){
            throw new Exception("Tag already exists");
        }

        GameObjects.Add(tagName, gameObject);
    }    

    public static Spatial GetGameObject(string tagName){
        return GameObjects[tagName];
    }
}
