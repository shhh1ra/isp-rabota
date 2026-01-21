package com.example.autosalon

import android.os.Bundle
import android.widget.Toast
import androidx.activity.enableEdgeToEdge
import androidx.appcompat.app.AlertDialog
import androidx.appcompat.app.AppCompatActivity
import androidx.core.widget.addTextChangedListener
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.autosalon.databinding.ActivityMainBinding
import com.example.autosalon.databinding.DialogCarBinding

class MainActivity : AppCompatActivity() {
    private lateinit var binding: ActivityMainBinding
    private lateinit var adapter: CarAdapter

    private val cars = mutableListOf<Car>()
    private var nextId = 1L

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        enableEdgeToEdge()
        binding = ActivityMainBinding.inflate(layoutInflater)
        setContentView(binding.root)

        adapter = CarAdapter(
            onEdit = { car -> showCarDialog(car) },
            onDelete = { car -> deleteCar(car) }
        )

        binding.carRecycler.layoutManager = LinearLayoutManager(this)
        binding.carRecycler.adapter = adapter

        binding.addButton.setOnClickListener { showCarDialog(null) }
        binding.searchEdit.addTextChangedListener { applyFilter() }

        applyFilter()
    }

    private fun showCarDialog(existingCar: Car?) {
        val dialogBinding = DialogCarBinding.inflate(layoutInflater)
        dialogBinding.nameEdit.setText(existingCar?.name.orEmpty())

        val title = if (existingCar == null) "Добавить" else "Изменить"

        AlertDialog.Builder(this)
            .setTitle(title)
            .setView(dialogBinding.root)
            .setPositiveButton("Сохранить") { _, _ ->
                val name = dialogBinding.nameEdit.text.toString().trim()
                if (name.isBlank()) {
                    Toast.makeText(this, "Введите название", Toast.LENGTH_SHORT).show()
                    return@setPositiveButton
                }
                if (existingCar == null) {
                    cars.add(Car(nextId++, name))
                } else {
                    existingCar.name = name
                }
                applyFilter()
            }
            .setNegativeButton("Отмена", null)
            .show()
    }

    private fun deleteCar(car: Car) {
        cars.removeAll { it.id == car.id }
        applyFilter()
    }

    private fun applyFilter() {
        val query = binding.searchEdit.text.toString().trim()
        val filtered = if (query.isBlank()) {
            cars
        } else {
            cars.filter { it.name.contains(query, ignoreCase = true) }
        }
        adapter.submitList(filtered)
    }
}
