resource "digitalocean_droplet" "database" {
    count = var.database_count

    region = var.region
    image = "debian-13-x64"
    name = "database-${count.index}"
    size = var.droplet_size

    tags = [
        "database"
    ]
}
