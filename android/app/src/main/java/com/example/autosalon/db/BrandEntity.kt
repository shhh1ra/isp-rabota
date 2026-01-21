package com.example.autosalon.db

import androidx.room.Entity
import androidx.room.ForeignKey
import androidx.room.PrimaryKey

@Entity(
    tableName = "brands",
    foreignKeys = [
        ForeignKey(
            entity = ManufacturerEntity::class,
            parentColumns = ["id"],
            childColumns = ["manufacturerId"],
            onDelete = ForeignKey.CASCADE
        )
    ]
)
data class BrandEntity(
    @PrimaryKey(autoGenerate = true)
    val id: Int = 0,
    val name: String,
    val manufacturerId: Int
)
