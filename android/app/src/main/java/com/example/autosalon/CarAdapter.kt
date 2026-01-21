package com.example.autosalon

import android.view.LayoutInflater
import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView
import com.example.autosalon.databinding.ItemCarBinding

class CarAdapter(
    private val onEdit: (Car) -> Unit,
    private val onDelete: (Car) -> Unit
) : RecyclerView.Adapter<CarAdapter.CarViewHolder>() {

    private val items = mutableListOf<Car>()

    fun submitList(newItems: List<Car>) {
        items.clear()
        items.addAll(newItems)
        notifyDataSetChanged()
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): CarViewHolder {
        val binding = ItemCarBinding.inflate(LayoutInflater.from(parent.context), parent, false)
        return CarViewHolder(binding)
    }

    override fun onBindViewHolder(holder: CarViewHolder, position: Int) {
        holder.bind(items[position], onEdit, onDelete)
    }

    override fun getItemCount(): Int = items.size

    class CarViewHolder(private val binding: ItemCarBinding) : RecyclerView.ViewHolder(binding.root) {
        fun bind(item: Car, onEdit: (Car) -> Unit, onDelete: (Car) -> Unit) {
            binding.carName.text = item.name
            binding.root.setOnClickListener { onEdit(item) }
            binding.root.setOnLongClickListener {
                onDelete(item)
                true
            }
            binding.deleteButton.setOnClickListener { onDelete(item) }
        }
    }
}
