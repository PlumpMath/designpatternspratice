﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DonOfDesign.PraticeBuilder.LCDEquipment
{
    public class Equipment
    {
        private Machine m_machine;
        private List<Port> m_list;
        private string m_name;

        public Machine Machine
        {
            get { return m_machine; }
            set { m_machine = value; }
        }
        public List<Port> PortsList
        {
            get { return m_list; }            
        }        

        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }	

        public Equipment()
        {            
            m_list = new List<Port>();
        }

        public void AddPort(Port port)
        {
            m_list.Add(port);
        }

        public void Run()
        {
            Console.WriteLine("The Equipment {0} is running as below...",m_name);
            foreach (Port port in m_list)
            {
                port.Transfer();
            }
            m_machine.Run();
        }
    }
}
