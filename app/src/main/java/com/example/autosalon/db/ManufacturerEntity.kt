package com.example.autosalon.db

import androidx.room.Entity
import androidx.room.PrimaryKey

@Entity(tableName = "manufacturers")
data class ManufacturerEntity(
    @PrimaryKey(autoGenerate = true)
    val id: Int = 0,
    val name: String,
    val country: String
)
