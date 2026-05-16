resource "digitalocean_spaces_bucket" "frontend" {
    name = "frontend"
    region = var.region
    acl = "public-read"
}

resource "digitalocean_cdn" "cdn" {
    origin = digitalocean_spaces_bucket.frontend.bucket_domain_name
    custom_domain = var.domain
    certificate_name = digitalocean_certificate.certificate.name
}
