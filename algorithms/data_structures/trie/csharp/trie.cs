using System;
using System.Collections.Generic;
using System.Linq;

class TrieNode {
    public TrieNode[] children = new TrieNode[26];
    public bool isLeaf;

    public TrieNode()
    {
        isLeaf = false;
        for (int i = 0; i < 26; i++) {
            children[i] = null;
        }
    }
}

class Trie {
    private TrieNode root;

    public Trie() { root = new TrieNode(); }

    // Method to insert a key into the Trie
    public void Insert(string key)
    {
        TrieNode curr = root;
        foreach(char c in key)
        {
            if (curr.children[c - 'a'] == null) {
                curr.children[c - 'a'] = new TrieNode();
            }
            curr = curr.children[c - 'a'];
        }
        curr.isLeaf = true;
    }

    // Method to search a key in the Trie
    public bool Search(string key)
    {
        TrieNode curr = root;
        foreach(char c in key)
        {
            if (curr.children[c - 'a'] == null)
                return false;
            curr = curr.children[c - 'a'];
        }
        return curr.isLeaf;
    }

    // Method to check if a prefix exists in the Trie
    public bool isPrefix(string prefix)
    {
        TrieNode curr = root;
        foreach(char c in prefix)
        {
            if (curr.children[c - 'a'] == null)
                return false;
            curr = curr.children[c - 'a'];
        }
        return true;
    }
}

Trie trie = new Trie();
string[] arr
    = { "and", "ant", "do", "dad"};
foreach(string s in arr) { trie.Insert(s); }

// One by one search strings
string[] searchKeys = { "do", "gee", "bat" };
foreach(string s in searchKeys){
    
    if (trie.Search(s))
        Console.Write("true ");
    else
        Console.Write("false ");
}
Console.WriteLine();
// One by one search for prefixes
string[] prefixKeys = { "ge", "ba", "do", "de" };
foreach(string s in prefixKeys){
    
    if (trie.isPrefix(s))
        Console.Write("true ");
    else
        Console.Write("false ");
}