using System;
using System.IO;
using System.Collections.Generic;

namespace GroceryStore
{
    public class InventoryRepository
    {
        private readonly string _filename = "list.txt";

        public void SaveItem(Item item)
        {
            using (StreamWriter writer = new StreamWriter(_filename, true))
            {
                writer.WriteLine(item.ToString());
            }
        }

        public List<Item> GetAllItems()
        {
            List<Item> items = new List<Item>();
            if (!File.Exists(_filename))
                return items;

            string[] lines = File.ReadAllLines(_filename);

            for (int lineIndex = 0; lineIndex < lines.Length; lineIndex++)
            {
                string line = lines[lineIndex];

                if (string.IsNullOrEmpty(line))
                    continue;

                string cat = "", itm = "", prcStr = "";
                int commas = 0;

                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == ',')
                    {
                        commas++;
                    }
                    else
                    {
                        if (commas == 0) cat += line[i];
                        else if (commas == 1) itm += line[i];
                        else if (commas == 2) prcStr += line[i];
                    }
                }

                if (cat == "" || itm == "" || prcStr == "")
                    continue;

                int p = 0;
                for (int i = 0; i < prcStr.Length; i++)
                {
                    if (prcStr[i] >= '0' && prcStr[i] <= '9')
                    {
                        p = p * 10 + (prcStr[i] - '0');
                    }
                }

                Item newItem = new Item();
                newItem.SetCategory(cat);
                newItem.SetName(itm);
                newItem.SetPrice(p);
                items.Add(newItem);
            }

            return items;
        }

        public void RewriteAllItems(List<Item> itemsToKeep)
        {
            using (StreamWriter writer = new StreamWriter(_filename, false))
            {

                for (int i = 0; i < itemsToKeep.Count; i++)
                {
                    writer.WriteLine(itemsToKeep[i].ToString());
                }
            }
           
        }
    }
}