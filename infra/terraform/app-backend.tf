resource "digitalocean_loadbalancer" "backend" {
    name = "backend-lb"
    region = var.region
    droplet_tag = "backend"

    forwarding_rule {
        entry_port = 443
        entry_protocol = "https"

        target_port = 80
        target_protocol = "http"

        certificate_name = digitalocean_certificate.certificate
    }
}

resource "digitalocean_droplet" "backend" {
    count = var.backend_count

    region = var.region
    image = "debian-13-x64"
    name = "backend-${count.index}"
    size = var.droplet_size

    vpc_uuid = digitalocean_vpc.main.id

    tags = [
        "backend"
    ]
}
