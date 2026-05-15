variable "do_api_token" {
    default = env("DIGITALOCEAN_API_TOKEN")
    sensitive = true
}
