using System;

class HashTable
{
    private int[] keys;
    private int[] values;
    private int capacity;
    private int size;

    public HashTable(int capacity)
    {
        this.capacity = capacity;
        keys = new int[capacity];
        values = new int[capacity];
    }

    private int Hash1(int key)
    {
        return key % capacity;
    }

    private int Hash2(int key)
    {
        return 7 - (key % 7);
    }

    private int Hash(int key, int i)
    {
        return (Hash1(key) + i * Hash2(key)) % capacity;
    }

    private void Rehash()
    {
        int newCapacity = capacity * 2;
        int[] newKeys = new int[newCapacity];
        int[] newValues = new int[newCapacity];

        for (int i = 0; i < capacity; i++)
        {
            if (keys[i] != 0)
            {
                int key = keys[i];
                int value = values[i];
                int index = Hash1(key);
                int j = 0;

                while (newKeys[index] != 0)
                {
                    j++;
                    index = Hash(key, j);
                }

                newKeys[index] = key;
                newValues[index] = value;
            }
        }

        keys = newKeys;
        values = newValues;
        capacity = newCapacity;
    }

    public void Insert(int key, int value)
    {
        if (size == capacity)
        {
            Rehash();
        }

        int index = Hash1(key);
        int i = 0;

        while (keys[index] != 0 && keys[index] != key)
        {
            i++;
            index = Hash(key, i);

            if (index == Hash1(key))
            {
                Rehash();
                index = Hash(key, i);
            }
        }

        keys[index] = key;
        values[index] = value;
        size++;
    }

    public int Search(int key)
    {
        int index = Hash1(key);
        int i = 0;

        while (keys[index] != 0)
        {
            if (keys[index] == key)
            {
                return values[index];
            }

            i++;
            index = Hash(key, i);

            if (index == Hash1(key))
            {
                break;
            }
        }

        return -1;
    }

    public void Display()
    {
        for (int i = 0; i < capacity; i++)
        {
            if (keys[i] != 0)
            {
                Console.WriteLine($"Key: {keys[i]}, Value: {values[i]}");
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        HashTable hashTable = new HashTable(10);

        hashTable.Insert(10, 100);
        hashTable.Insert(22, 220);
        hashTable.Insert(31, 310);

        int value = hashTable.Search(22);
        Console.WriteLine("Значение по ключу 22: " + value);

        Console.WriteLine("Хэш-таблица:");
        hashTable.Display();
    }
}
