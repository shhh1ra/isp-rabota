package com.example.autosalon.db

data class CarWithBrand(
    val id: Int,
    val vin: String,
    val modelYear: Int,
    val color: String,
    val price: Double,
    val brandId: Int,
    val brandName: String
)