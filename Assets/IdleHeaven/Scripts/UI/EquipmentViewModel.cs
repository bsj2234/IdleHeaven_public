using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace IdleHeaven
{
    public class EquipmentViewModel :MonoBehaviour, INotifyPropertyChanged
    {
        [SerializeField] Equipments _equipments;

        public event PropertyChangedEventHandler PropertyChanged;

        public Equipments Equipments
        {
            get => _equipments;
            set
            {
                _equipments = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(Equipments)));
                }
            }
        }

        private void Start()
        {
            _equipments.OnEquipmentsChagned += HandleEquipmentsChagned;
        }

        public Item GetItem(EquipmentType slot)
        {
            return _equipments.GetEquippedItem(slot);
        }

        public void Unequip(EquipmentType slot)
        {
            _equipments.Unequip(slot);
        }


        private void HandleEquipmentsChagned(EquipmentType slot, Item item, Equipments equipments)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(Equipments)));
            }
        }
    }
}