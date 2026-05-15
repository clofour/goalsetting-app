data "digitalocean_image" "database" {
    name = "database"
}

resource "digitalocean_droplet" "database" {
    count = var.database_count

    region = var.region
    image = data.digitalocean_image.database.id
    name = "database-${count.index}"
    size = var.droplet_size

    vpc_uuid = digitalocean_vpc.main.id

    tags = [
        "database"
    ]
}
