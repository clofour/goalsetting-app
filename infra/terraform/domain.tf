locals {
    records = [
        {name = var.frontend_subdomain, value = digitalocean_cdn.cdn.ip},
        {name = var.backend_subdomain, value = digitalocean_loadbalancer.backend.ip}
    ]
}

data "digitalocean_domain" "domain" {
    name = var.domain
}

resource "digitalocean_record" "main" {
    for_each = local.records

    type = "A"
    domain = var.domain
    name = each.value.type
    value = each.value.value
}

resource "digitalocean_certificate" "certificate" {
    name = "certificate"
    type = "lets_encrypt"
    domains = [
        var.domain
    ]
}