import { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router';
import {
    Button,
    Group,
    PasswordInput,
    TextInput,
    Checkbox,
    Tabs,
    Alert,
    Progress,
    Paper,
    Container,
    Title
} from '@mantine/core';
import { useForm, isEmail } from '@mantine/form';
import { useInputState } from '@mantine/hooks';
import zxcvbn from "zxcvbn";
import { IconLogin2, IconUserPlus, IconExclamationCircle, IconInfoCircle } from '@tabler/icons-react';
import Logo from "@/components/Logo";


const accessCodeRegex = new RegExp("^[a-zA-Z]+$");
const usernameRegex = new RegExp("^[a-zA-Z0-9]+$");
const emailRegex = /^\w+([.-]?\w+)*@\w+([.-]?\w+)*(\.\w{2,})+$/;
const passwordRegex = new RegExp("^[a-zA-Z0-9]+$");

function getStrengthColor(strength) {
    switch (strength) {
        case 0:
            return "red";
        case 1:
            return "orange";
        case 2:
            return "yellow";
        case 3:
            return "green";
        case 4:
            return "blue";
    }
}

function Authentication() {
    const navigate = useNavigate();
    const { tabValue } = useParams();
    const [consent, setConsent] = useInputState(false);
    const [alert, setAlert] = useState({ status: undefined, message: "" });
    const [activeTab, setActiveTab] = useState("signin");
    const [loading, setLoading] = useState(false);

    let [strength, setStrength] = useState(0);
    let [passwordColor, setPasswordColor] = useState("red");

    const signinForm = useForm({
        mode: "uncontrolled",
        initialValues: {
            username: "",
            password: ""
        },
        validate: (values) => ({
            username:
                values.username.length < 3 || values.username.length > 20 ? "Username must contain between 3 and 20 characters." :
                    !usernameRegex.test(values.username) ? "Username must only contain letters and numbers." : null,
            password:
                values.password.length < 16 || values.password.length > 64 ? "Password must contain between 16 to 64 characters." :
                    !passwordRegex.test(values.password) ? "Password must only contain letters and numbers." : null
        }),
    });
    const signupForm = useForm({
        mode: "uncontrolled",
        initialValues: {
            accessCode: "",
            username: "",
            email: "",
            password: "",
            termsOfService: false,
        },
        validate: (values) => ({
            accessCode:
                values.accessCode.length < 3 || values.accessCode.length > 20 ? "Access code must contain between 3 and 20 characters." :
                    !accessCodeRegex.test(values.accessCode) ? "Access code must only contain letters." : null,
            username:
                values.username.length < 3 || values.username.length > 20 ? "Username must contain between 3 and 20 characters." :
                    !usernameRegex.test(values.username) ? "Username must only contain letters and numbers." : null,
            email:
                !emailRegex.test(values.email) ? "This email is invalid." : null,
            password:
                values.password.length < 16 || values.password.length > 64 ? "Password must contain between 16 to 64 characters." :
                    !passwordRegex.test(values.password) ? "Password must only contain letters and numbers." : null,
        }),
        onValuesChange: (values) => {
            const newStrength = zxcvbn(values.password).score;
            setStrength(newStrength);
            setPasswordColor(getStrengthColor(newStrength));
        }
    });

    useEffect(() => {
        const passwordStrengthInterval = setInterval(() => {
            const newStrength = zxcvbn(signupForm.getValues().password).score;
            setStrength(newStrength);
            setPasswordColor(getStrengthColor(newStrength));
        }, 1000)
        return () => clearInterval(passwordStrengthInterval);
    }, []);

    async function post(values, endpoint) {
        setLoading(true);
        const response = await fetch(endpoint, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "X-CSRF-Token": "clofour.com"
            },
            body: JSON.stringify(values)
        });
        if (response.status >= 500) {
            setAlert({ status: "Error", message: "An error has ocurred. Please try again later." });
        }
        else if (!response.ok) {
            setAlert({ status: "Error", message: response.text() });
        }
        else {
            setAlert({ status: "Success", message: response.text() });
        };
        setLoading(false);
        return response.ok;
    }

    async function handleSigninSubmit(values) {
        const success = await post(values, "/api/auth/signin");
        if (success) {
            window.location.href = "/services/messaging";
        }
    };
    async function handleSignupSubmit(values) {
        const success = await post(values, "/api/auth/signup");
        if (success) {
            signupForm.reset();
            setActiveTab("signin");
        }
    };

    return (
        <Container size={420} my={40}>

            <Paper withBorder shadow="sm" p={22} mt={30} radius="md"> {
                <>

                <Logo />


                <Tabs value={tabValue} onChange={(value) => navigate(`/auth/${value}`)}>

                    <Alert variant="light" color={alert.status === "Error" ? "red" : "green"} title={alert.status} icon={alert.status === "Error" ? <IconExclamationCircle /> : <IconInfoCircle />} mt="xs" mb="sm" hidden={(alert.status === undefined)}>{alert.message}</Alert>

                    <Tabs.List grow>
                        <Tabs.Tab value="signin" leftSection={<IconLogin2 size={12} />} disabled={loading}>
                            Sign In
                        </Tabs.Tab>
                        <Tabs.Tab value="signup" leftSection={<IconUserPlus size={12} />} disabled={loading}>
                            Sign Up
                        </Tabs.Tab>
                    </Tabs.List>

                    <Tabs.Panel value="signin">

                        <form onSubmit={signinForm.onSubmit(handleSigninSubmit)}>
                            <TextInput
                                mt="md"
                                withAsterisk
                                label="Username"
                                placeholder="Your username"
                                disabled={loading}
                                key={signinForm.key("username")}
                                {...signinForm.getInputProps("username")}
                            />

                            <PasswordInput
                                mt="md"
                                withAsterisk
                                label="Password"
                                placeholder="Your password"
                                disabled={loading}
                                key={signinForm.key("password")}
                                {...signinForm.getInputProps("password")}
                            />

                            <Group justify="flex-end" mt="md">
                                <Button type="submit" disabled={loading}>Submit</Button>
                            </Group>
                        </form>

                    </Tabs.Panel>

                    <Tabs.Panel value="signup">

                        <form onSubmit={signupForm.onSubmit(handleSignupSubmit)}>
                            <TextInput
                                mt="md"
                                withAsterisk
                                label="Access Code"
                                placeholder="Your access code"
                                disabled={loading}
                                key={signupForm.key("accessCode")}
                                {...signupForm.getInputProps("accessCode")}
                            />

                            <TextInput
                                mt="md"
                                withAsterisk
                                label="Username"
                                placeholder="Your username"
                                disabled={loading}
                                key={signupForm.key("username")}
                                {...signupForm.getInputProps("username")}
                            />

                            <TextInput
                                mt="md"
                                withAsterisk
                                label="Email"
                                placeholder="Your email"
                                disabled={loading}
                                key={signupForm.key("email")}
                                {...signupForm.getInputProps("email")}
                            />

                            <PasswordInput
                                mt="md"
                                withAsterisk
                                label="Password"
                                placeholder="Your password"
                                disabled={loading}
                                key={signupForm.key("password")}
                                {...signupForm.getInputProps("password")}
                            />
                            <Group grow gap={5} mt="xs">
                                <Progress
                                    size="xs"
                                    color={passwordColor}
                                    value={signupForm.getValues().password.length > 0 ? 100 : 0}
                                    transitionDuration={0}
                                />
                                <Progress size="xs" color={passwordColor} transitionDuration={0} value={strength >= 1 ? 100 : 0} />
                                <Progress size="xs" color={passwordColor} transitionDuration={0} value={strength >= 2 ? 100 : 0} />
                                <Progress size="xs" color={passwordColor} transitionDuration={0} value={strength >= 3 ? 100 : 0} />
                                <Progress size="xs" color={passwordColor} transitionDuration={0} value={strength >= 4 ? 100 : 0} />
                            </Group>

                            <Checkbox
                                mt="md"
                                label="I agree to the Terms of Service and Privacy Policy."
                                key={signupForm.key("termsOfService")}
                                {...signupForm.getInputProps("termsOfService", { type: "checkbox" })}
                                disabled={loading}
                            />

                            <Group justify="flex-end" mt="md">
                                <Button type="submit" disabled={!signupForm.getValues().termsOfService || loading} loading={loading}>Submit</Button>
                            </Group>
                        </form>

                    </Tabs.Panel>

                </Tabs>

                </>
                
            } </Paper>

        </Container>

    )
}

export default Authentication;