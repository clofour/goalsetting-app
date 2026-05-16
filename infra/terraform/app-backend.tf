resource "digitalocean_loadbalancer" "backend" {
    name = "backend-lb"
    region = var.region
    droplet_tag = "backend"

    forwarding_rule {
        entry_port = 443
        entry_protocol = "https"

        target_port = 80
        target_protocol = "http"

        certificate_name = digitalocean_certificate.certificate.name
    }
}

data "digitalocean_images" "backend" {
    filter {
        key = "name"
        match_by = "substring"
        values = ["backend"]
    }
    sort {
        key = "name"
        direction = "desc"
    }

}

resource "digitalocean_droplet" "backend" {
    count = var.backend_count

    region = var.region
    image = data.digitalocean_images.backend.images[0].id
    name = "backend-${count.index}"
    size = var.droplet_size

    vpc_uuid = digitalocean_vpc.main.id

    tags = [
        "backend"
    ]
}
